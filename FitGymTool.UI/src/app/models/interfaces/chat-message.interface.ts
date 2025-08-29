export interface ChatMessage {
  content: string;
  isBot: boolean;
  isTyping?: boolean;
  contentType?: 'text' | 'markdown-table' | 'markdown';
  userIntent?: string;
  followupQuestions?: string[];
}
