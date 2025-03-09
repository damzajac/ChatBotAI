import { Component, input, output } from '@angular/core';
import { ChatMessage } from '../../models/chat-message.model';
import { ChatbotService } from '../../services/chatbot.service';

@Component({
  selector: 'app-chat-message',
  imports: [],
  template: `
    <div class="flex justify-start">
      <div class="bg-gray-100 rounded-lg px-4 py-2 max-w-[80%]">
        <p class="text-gray-900 text-sm">{{ chatMessage().question }}</p>
      </div>
    </div>
    @if (chatMessage().answer) {
      <div class="flex justify-end">
        <div class="bg-blue-500 rounded-lg px-4 py-2 mt-4 max-w-[80%] " 
          [class]="chatMessage().isLiked == true ? 'border-2 border-green-700' : chatMessage().isLiked == false ? 'border-2 border-red-700' : '' ">
          <p class="text-white text-sm">{{ chatMessage().answer }} </p>
          <div clas="grid justify-items-stretch">
              <div class="justify-self-end">
                @if (chatMessage().isGenerated != false) { 
                  <button (click)='onLikeClicked()' class="bg-white text-blue-900 font-semibold px-1 py-1 mt-2 rounded-full m-1 hover:bg-blue-100 transition duration-300 text-center text-xs">Like</button>
                  <button (click)='onDislikeClicked()' class="bg-white text-blue-900 font-semibold px-1 py-1 mt-2 rounded-full m-1 hover:bg-blue-100 transition duration-300 text-center text-xs">Dislike</button>
                }
                @else {
                  <button (click)='btnStopGeneratingClicked.emit()' class="bg-white text-blue-900 font-semibold px-1 py-1 mt-2 rounded-full m-1 hover:bg-blue-100 transition duration-300 text-center text-xs">Stop generating</button>
                }
              </div>
            </div>
        </div>
      </div>
    }

  `,
  styles: ``
})
export class ChatMessageComponent {
  chatMessage = input.required<ChatMessage>();
  btnStopGeneratingClicked = output();

  constructor (private apiService: ChatbotService) {}
  
  onLikeClicked() {
    this.chatMessage().isLiked = true;
    this.apiService.updateChatMessage(this.chatMessage()).subscribe();
  }

  onDislikeClicked() {
    this.chatMessage().isLiked = false;
    this.apiService.updateChatMessage(this.chatMessage()).subscribe();
  }
}
