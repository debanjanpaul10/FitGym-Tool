export interface ChatMessage {
  content: string;
  isBot: boolean;
  isTyping?: boolean;
  contentType?: 'text' | 'markdown-table';
  userIntent?: string;
}
