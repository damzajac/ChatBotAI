import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChatMessage } from '../models/chat-message.model';
import { environment } from '../../environment/environment'

@Injectable({
  providedIn: 'root'
})
export class ChatbotService {

  private http: HttpClient = inject(HttpClient);

  getChatMessages() : Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(`${environment.baseUrl}${environment.chatMessagesEndpoint}`);
  }

  updateChatMessage(chatMessage: ChatMessage) : Observable<ChatMessage> {
    return this.http.put<ChatMessage>(`${environment.baseUrl}${environment.chatMessagesEndpoint}/${chatMessage.id}`, chatMessage);
  }
}
