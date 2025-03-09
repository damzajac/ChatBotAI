import { Injectable, NgZone } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { ChatEvent } from '../models/chat-event.model';
import { environment } from '../../environment/environment'

@Injectable({
  providedIn: 'root'
})
export class EventSourceService {
    private eventSource!: EventSource;
    private eventNames = ['idGenerated', 'answerPartGenerated', 'fullAnswerGenerated'];

    constructor(private zone: NgZone) {}

    connectToServerSentEvents(question: string): Observable<ChatEvent> {
        this.eventSource = new EventSource(`${environment.baseUrl}${environment.conversationEndpoint}${question}`);

        return new Observable((subscriber: Subscriber<ChatEvent>) => {
            this.eventNames.forEach((event: string) => {
                this.eventSource.addEventListener(event, data => {
                    this.zone.run(() => subscriber.next(data));
                });
            });
        });
    }

    close(): void {
        if (!this.eventSource) {
            return;
        }

        this.eventSource.close();
    }
}
