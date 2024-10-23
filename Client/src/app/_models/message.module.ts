export interface Message {
  id: number;
  senderId: string;
  recipientId: string;
  content: string | null;
  fileUrl: string | null;
  fileType: string | null;
  sentAt: string;
}
