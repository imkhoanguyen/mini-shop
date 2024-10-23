import { Injectable } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { ReviewDto } from '../_models/review';
@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private hubConnection?: HubConnection;
  private hubUrl = environment.hubsUrl;
  public reviewReceived = new Subject<ReviewDto>();
  public reviewUpdated = new Subject<ReviewDto>();
  public reviewDeleted = new Subject<number>();
  public replyReceived = new Subject<ReviewDto>();
  startConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + '/review')
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('start');
        this.addReviewListener();
        this.updateReviewListener();
        this.deleteReviewListener();
        this.addReplyListener();
      })
      .catch((err) => console.log('error when start hub', err));
  }

  stopHubConnection() {
    if (this.hubConnection?.state === HubConnectionState.Connected) {
      this.hubConnection.stop().catch((er) => console.log(er));
    }
  }

  addReviewListener() {
    this.hubConnection?.on('add-review', (data: ReviewDto) => {
      this.reviewReceived.next(data);
    });
  }

  updateReviewListener() {
    this.hubConnection?.on('edit-review', (data: ReviewDto) => {
      this.reviewUpdated.next(data);
    });
  }

  deleteReviewListener() {
    this.hubConnection?.on('delete-review', (reviewId: number) => {
      this.reviewDeleted.next(reviewId);
    });
  }

  addReplyListener() {
    this.hubConnection?.on('add-reply', (data: ReviewDto) => {
      this.replyReceived.next(data);
    });
  }
}
