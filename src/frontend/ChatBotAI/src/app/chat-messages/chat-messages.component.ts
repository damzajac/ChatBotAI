import { Component, OnDestroy, signal } from '@angular/core';
import { ChatMessage } from '../models/chat-message.model';
import { ChatMessageComponent } from "./chat-message/chat-message.component";
import { ChatbotService } from '../services/chatbot.service';
import { SubscriptionLike } from 'rxjs';
import { EventSourceService } from '../services/event-source.service';
import { ChatEvent } from '../models/chat-event.model';
import { APP_CONSTANTS } from '../constants/constants';

@Component({
  selector: 'app-chat-messages',
  imports: [ChatMessageComponent],
  template: `
    <div class="flex flex-col">
      <div class="flex-grow overflow-y-auto">
          <div class="flex flex-col mb-4 gap-4 py-4">
            @for (chatMessage of chatMessages(); track chatMessage.id ) {
              <app-chat-message [chatMessage]="chatMessage" (btnStopGeneratingClicked)="onClickedStopGenerating()" />
            }
          </div>
      </div>
      <div class="flex justify-center items-center h-16">
          <input type="text" [value]='question' (change)='onChangedQuestion($event)' class="border border-gray-300 rounded-lg py-2 px-4 w-full max-w-lg mr-4" placeholder="Type a message...">
          <button (click)='sendQuestion()' class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Send</button>
      </div>
    </div>
  `,
  styles: ``
})
export class ChatMessagesComponent implements OnDestroy {
  question: string = '';
  chatMessages = signal<ChatMessage[]>([]);
  newChatMessage : ChatMessage = { }
  eventSourceSubscription!: SubscriptionLike;

  constructor(private apiService: ChatbotService, private eventSourceService: EventSourceService) {}

  onChangedQuestion(event: Event) {
    this.question = (event.target as HTMLInputElement).value;
  }

  onClickedStopGenerating() {
    this.finishGeneratingAnswer();
  }

  sendQuestion () {
    if (!this.question || this.question.trim() === '') {
      return;
    }
    
    this.finishGeneratingAnswer();

    const givenQuestion = this.question;
    this.question = '';

    this.newChatMessage = {
      question: givenQuestion,
      answer: '',
      isGenerated: false
    };
 
    this.eventSourceSubscription = this.eventSourceService.connectToServerSentEvents(givenQuestion)
      .subscribe({
        next: chatEvent => { 
          this.handleEvent(chatEvent);              
        }
      });
  }

  handleEvent(chatEvent: ChatEvent) {
    if (chatEvent.type == APP_CONSTANTS.ID_GENERATED_EVENT) {
      const idGEneratedEvent = JSON.parse(chatEvent.data) as { id: string };
      this.newChatMessage!.id = idGEneratedEvent.id;
      this.chatMessages.update(values => {
        return [...values, this.newChatMessage as ChatMessage];
     });
    }
    else if (chatEvent.type == APP_CONSTANTS.ANSWER_PART_GENERATED_EVENT) {
      const textPartGeneratedEvent = JSON.parse(chatEvent.data) as { text: string };
      this.newChatMessage.answer += textPartGeneratedEvent.text;
    }
    else if (chatEvent.type == APP_CONSTANTS.FULL_ANSWER_GENERATED_EVENT) {
        this.finishGeneratingAnswer();
    }
  }

  finishGeneratingAnswer() {
    this.newChatMessage.isGenerated = true;
    this.closeEventSource();
  }

  closeEventSource() {
    this.eventSourceSubscription?.unsubscribe();
    this.eventSourceService?.close();
  }

  ngOnInit() {
    this.apiService.getChatMessages().subscribe((res: any) => {
      this.chatMessages.set(Object.values(res) as ChatMessage[]);
    })
  }

  ngOnDestroy(): void {
    this.closeEventSource();
  }
}