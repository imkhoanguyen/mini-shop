
export interface FileMessageDto {
  id: number;
  fileUrl: string | null;
  fileType: string | undefined;
}
export interface MessageBase {
  senderId: string;
  recipientIds: string[];
  content: string | null;
}
export interface MessageAdd extends MessageBase {}
export interface MessageDto extends MessageBase {
  id: number;
  sentAt: string;
  Files: FileMessageDto[];
  isReplied: boolean;
  repliedById: string | null;
}
