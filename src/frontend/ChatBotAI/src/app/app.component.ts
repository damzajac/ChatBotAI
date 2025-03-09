import { Component } from '@angular/core';
import { ChatMessagesComponent } from "./chat-messages/chat-messages.component";

@Component({
  selector: 'app-root',
  imports: [ChatMessagesComponent],
  template: `
    <div class='flex justify-center max-w-lg flex-col mx-auto'>
      <h2 class="text-5xl font-semibold tracking-tight text-center text-gray-700 m-8">
        Welcome to {{title}}!
      </h2>
      <app-chat-messages />
    </div>
  `,
  styles: [],
})
export class AppComponent {
  title = 'ChatBotAI';
}
