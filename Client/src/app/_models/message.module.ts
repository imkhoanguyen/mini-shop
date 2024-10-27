export interface Message {
  id: number;
  senderId: string;
  recipientIds: string[];
  content: string | null;
  fileUrl: string | null;
  fileType: string | undefined;
  repliedById: string | null;
  isReplied: boolean;
  sentAt: string;
}
