import {
  Component,
  inject,
  signal,
  WritableSignal,
  ViewChild,
  ElementRef,
  AfterViewChecked,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { DockModule } from 'primeng/dock';
import { TooltipModule } from 'primeng/tooltip';
import { ButtonModule } from 'primeng/button';
import { MsalService } from '@azure/msal-angular';
import { AccountInfo } from '@azure/msal-browser';
import { ChatComponentSuggestions } from '../chat-component-suggestions/chat-component-suggestions';

import { ToasterService } from '@core/services/toaster.service';
import { AiApiService } from '@services/ai-services-api.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ChatMessageRequestDTO } from '@models/DTO/chat-message-request-dto.model';
import {
  CommonApplicationConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { AIChatbotResponseDTO } from '@models/DTO/ai-chatbot-response-dto.model';
import { ChatMessage } from '@models/interfaces/chat-message.interface';
import { Utilities } from '@core/helpers/utilities-helper';
import { SampleChatbotPromptsDTO } from '@models/DTO/sample-chatbot-prompts-dto.model';
import { SuggestionItem } from '@models/interfaces/suggestion-item.interface';

/**
 * @component
 * AI-powered chat component that provides an interactive chatbot interface for the FitGym application.
 *
 * This component offers a comprehensive chat experience with features including:
 * - Real-time AI conversation with typewriter effects for engaging user experience
 * - Support for multiple content types (text, markdown, SQL tables)
 * - Intelligent followup question suggestions based on AI responses
 * - Sample prompt suggestions to help users get started
 * - Expandable/collapsible chat window with responsive design
 * - Message copying functionality and scroll management
 * - Integration with Microsoft Authentication Library (MSAL) for user context
 * - Error handling and loading states for robust user experience
 *
 * The component handles different types of AI responses:
 * - SQL queries: Displays results as formatted tables without typewriter effect
 * - General responses: Shows markdown content with smooth typewriter animation
 * - Followup questions: Presents clickable suggestion bubbles for continued conversation
 */
@Component({
  selector: 'app-chat-component',
  standalone: true,
  imports: [
    CommonModule,
    DockModule,
    TooltipModule,
    ButtonModule,
    ChatComponentSuggestions,
  ],
  templateUrl: './chat-component.html',
  styleUrl: './chat-component.scss',
})
export class ChatComponent implements AfterViewChecked, OnInit {
  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;
  @ViewChild('messageInput') private messageInput!: ElementRef;

  protected isChatOpen: WritableSignal<boolean> = signal(false);
  protected isProcessing: WritableSignal<boolean> = signal(false);
  protected isExpanded: WritableSignal<boolean> = signal(false);
  protected AIMessages = CommonApplicationConstants.AIConstants;
  protected parseMarkdownTable = Utilities.ParseMarkdownTable;
  protected parseMarkdownToHtml = Utilities.ParseMarkdownToHtml;
  protected sampleChatbotPrompts: WritableSignal<SampleChatbotPromptsDTO[]> =
    signal([]);

  private readonly _toasterService: ToasterService = inject(ToasterService);
  private readonly _aiApiService: AiApiService = inject(AiApiService);
  private readonly _msalService: MsalService = inject(MsalService);
  private shouldScrollToBottom = true;
  private readonly currentUserProfile: WritableSignal<AccountInfo | null> =
    signal(null);

  protected showScrollToBottomButton = false;
  protected messages: WritableSignal<Array<ChatMessage>> = signal([]);

  ngOnInit(): void {
    this.loadUserProfile();
    this.initializeMessages();
    this.getSampleAiPrompts();

    setTimeout(() => {
      if (!this.currentUserProfile()?.name) {
        this.loadUserProfile();
      }
    }, 500);
  }

  /**
   * Handles automatic scrolling to bottom after view updates when required.
   */
  ngAfterViewChecked(): void {
    if (this.shouldScrollToBottom) {
      this.scrollToBottom();
    }
  }

  /**
   * Handles scroll events in the messages container to determine scroll position and show/hide scroll-to-bottom button.
   */
  protected onMessagesScroll(): void {
    if (this.messagesContainer) {
      const element = this.messagesContainer.nativeElement;
      const isAtBottom =
        element.scrollHeight - element.scrollTop <= element.clientHeight + 10;
      this.shouldScrollToBottom = isAtBottom;
      this.showScrollToBottomButton = !isAtBottom;
    }
  }

  /**
   * Manually scrolls the chat messages to the bottom and hides the scroll-to-bottom button.
   */
  protected scrollToBottomManually(): void {
    this.shouldScrollToBottom = true;
    this.scrollToBottom();
    this.showScrollToBottomButton = false;
  }

  /**
   * Copies the specified message text to the clipboard and shows a success notification.
   * @param message - The message text to copy to clipboard
   */
  protected copyUserMessage(message: string): void {
    navigator.clipboard
      .writeText(message)
      .then(() => {
        this._toasterService.showSuccess(
          ToasterSuccessMessages.Common.MessageCopiedSuccess
        );
      })
      .catch((err) => {
        console.error(err);
        this._toasterService.showError(err);
      });
  }

  /**
   * Toggles the chat window open/closed state and ensures proper scrolling after state change.
   */
  protected toggleChat(): void {
    this.isChatOpen.set(!this.isChatOpen());
    setTimeout(() => {
      this.shouldScrollToBottom = true;
      this.scrollToBottom();
    }, 100);
  }

  /**
   * Closes the chat window and resets the expanded state.
   */
  protected closeChat(): void {
    this.isChatOpen.set(false);
    this.isExpanded.set(false);
  }

  /**
   * Toggles the chat window between normal and expanded size modes.
   */
  protected toggleExpand(): void {
    this.isExpanded.set(!this.isExpanded());
    setTimeout(() => {
      this.shouldScrollToBottom = true;
      this.scrollToBottom();
    }, 100);
  }

  /**
   * Refreshes the chat by reloading user profile, reinitializing messages, and resetting processing state.
   */
  protected refreshChats(): void {
    this.loadUserProfile();
    this.initializeMessages();
    this.isProcessing.set(false);
    this.shouldScrollToBottom = true;
  }

  /**
   * Processes and sends user messages to the AI service, handling input validation and UI state updates.
   * @param event - The event object from the input field or send button
   */
  protected sendMessage(event: any): void {
    let input =
      event.target.tagName === 'INPUT'
        ? event.target
        : event.target.previousElementSibling;

    if (!input && this.messageInput) {
      input = this.messageInput.nativeElement;
    }

    const message = input.value.trim();
    if (message && !this.isProcessing()) {
      this.isProcessing.set(true);
      this.shouldScrollToBottom = true;

      this.messages.update((msgs) => [
        ...msgs,
        { content: message, isBot: false, contentType: 'text' },
      ]);
      input.value = '';

      this.messages.update((msgs) => [
        ...msgs,
        { content: '', isBot: true, isTyping: true, contentType: 'text' },
      ]);

      const request: ChatMessageRequestDTO = {
        chatMessage: message,
      };
      this.sendAiMessageToApiAsync(request);
    }
  }

  /**
   * Inserts a sample prompt text into the message input field and focuses it.
   * @param promptText - The sample prompt text to insert
   */
  protected insertSamplePrompt(promptText: string): void {
    if (this.messageInput && !this.isProcessing()) {
      this.messageInput.nativeElement.value = promptText;
      this.messageInput.nativeElement.focus();
    }
  }

  /**
   * Inserts a followup question text into the message input field and focuses it.
   * @param questionText - The followup question text to insert
   */
  protected insertFollowupQuestion(questionText: string): void {
    if (this.messageInput && !this.isProcessing()) {
      this.messageInput.nativeElement.value = questionText;
      this.messageInput.nativeElement.focus();
    }
  }

  /**
   * Converts sample prompts to SuggestionItem format for the suggestions component.
   */
  protected get samplePromptSuggestions(): SuggestionItem[] {
    return this.sampleChatbotPrompts().map((prompt) => ({
      id: prompt.promptName,
      text: prompt.promptName,
      category: prompt.area,
      icon: 'pi pi-lightbulb',
    }));
  }

  /**
   * Converts followup questions to SuggestionItem format for the suggestions component.
   */
  protected convertFollowupQuestions(questions: string[]): SuggestionItem[] {
    return questions.map((question, index) => ({
      id: `followup-${index}`,
      text: question,
      icon: 'pi pi-question-circle',
    }));
  }

  /**
   * Handles sample prompt selection from the suggestions component.
   */
  protected onSamplePromptSelected(suggestion: SuggestionItem): void {
    this.insertSamplePrompt(suggestion.text);
  }

  /**
   * Handles followup question selection from the suggestions component.
   */
  protected onFollowupQuestionSelected(suggestion: SuggestionItem): void {
    this.insertFollowupQuestion(suggestion.text);
  }

  /**
   * Determines if the message at the given index is the latest bot message.
   * @param index - The index of the message to check
   * @returns true if this is the latest bot message, false otherwise
   */
  protected isLatestBotMessage(index: number): boolean {
    const messages = this.messages();
    const currentMessage = messages[index];

    if (!currentMessage?.isBot) {
      return false;
    }

    // Find the last bot message index
    for (let i = messages.length - 1; i >= 0; i--) {
      if (messages[i].isBot && !messages[i].isTyping) {
        return i === index;
      }
    }

    return false;
  }

  // #region PRIVATE METHODS

  /**
   * Fetches sample AI prompts from the API service to display as quick-start options.
   */
  private getSampleAiPrompts(): void {
    this._aiApiService.GetSamplePromptsForChatbotAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.sampleChatbotPrompts.set(response?.responseData);
        }
      },
      error: (error: Error) => {
        console.error(error);
        this._toasterService.showError(error.message);
      },
    });
  }

  /**
   * Scrolls the messages container to the bottom position.
   */
  private scrollToBottom(): void {
    try {
      if (this.messagesContainer) {
        const element = this.messagesContainer.nativeElement;
        element.scrollTop = element.scrollHeight;
      }
    } catch (err) {
      console.error(err);
    }
  }

  /**
   * Loads the current user profile from MSAL authentication service and updates the greeting message.
   */
  private loadUserProfile(): void {
    const activeAccount = this._msalService.instance.getActiveAccount();
    this.currentUserProfile.set(activeAccount);

    if (!activeAccount) {
      const accounts = this._msalService.instance.getAllAccounts();
      if (accounts.length > 0) {
        this.currentUserProfile.set(accounts[0]);
      }
    }

    this.updateGreetingMessage();
  }

  /**
   * Updates the initial greeting message with the current user's name if available.
   */
  private updateGreetingMessage(): void {
    const messages = this.messages();
    if (messages.length > 0 && messages[0].isBot) {
      const updatedGreeting = Utilities.GetChatbotGreetingMessage(
        this.currentUserProfile()?.name
      );
      if (messages[0].content !== updatedGreeting) {
        this.messages.update((msgs) => {
          const updatedMsgs = [...msgs];
          updatedMsgs[0] = {
            ...updatedMsgs[0],
            content: updatedGreeting,
          };
          return updatedMsgs;
        });
      }
    }
  }

  /**
   * Initializes the messages array with a personalized greeting message from the chatbot.
   */
  private initializeMessages(): void {
    this.messages.set([
      {
        content: Utilities.GetChatbotGreetingMessage(
          this.currentUserProfile()?.name
        ),
        isBot: true,
        contentType: 'text',
      },
    ]);
  }

  /**
   * Sends the user message to the AI API service and handles the response or error scenarios.
   * @param request - The chat message request containing the user's message
   */
  private sendAiMessageToApiAsync(request: ChatMessageRequestDTO): void {
    this._aiApiService.RespondAsync(request).subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.handleAiResponse(response.responseData);
        } else {
          this.handleErrorResponse(this.AIMessages.AIFailedMessage);
        }
      },
      error: (error: any) => {
        console.error(error);

        let errorMessage = this.AIMessages.SendMessageFailed;
        if (error?.error?.errors?.chatMessage) {
          errorMessage = error.error.errors.chatMessage[0];
        } else if (error?.message) {
          errorMessage = error.message;
        }

        this.showErrorAsAiResponse();
        this._toasterService.showError(errorMessage);
      },
    });
  }

  /**
   * Handles error responses by displaying error messages and updating UI state.
   * @param errorMessage - The error message to display
   */
  private handleErrorResponse(errorMessage: string): void {
    this.showErrorAsAiResponse();
    this._toasterService.showError(errorMessage);
  }

  /**
   * Displays error messages as AI responses in the chat interface and resets processing state.
   */
  private showErrorAsAiResponse(): void {
    const messages = this.messages();
    const lastMessageIndex = messages.length - 1;

    if (messages[lastMessageIndex]?.isTyping) {
      this.messages.update((msgs) => {
        const updatedMsgs = [...msgs];
        updatedMsgs[lastMessageIndex] = {
          content: `Error: ${CommonApplicationConstants.AIConstants.AIFailedMessage}`,
          isBot: true,
          isTyping: false,
          contentType: 'text',
        };
        return updatedMsgs;
      });
    } else {
      this.messages.update((msgs) => [
        ...msgs,
        {
          content: `Error: ${CommonApplicationConstants.AIConstants.AIFailedMessage}`,
          isBot: true,
          isTyping: false,
          contentType: 'text',
        },
      ]);
    }

    this.isProcessing.set(false);
    this.shouldScrollToBottom = true;
  }

  /**
   * Routes AI responses to appropriate handlers based on the user intent (SQL or general responses).
   * @param aiChatbotResponse - The AI chatbot response containing content and metadata
   */
  private handleAiResponse(aiChatbotResponse: AIChatbotResponseDTO): void {
    if (aiChatbotResponse.userIntent === 'SQL') {
      this.handleSqlResponse(aiChatbotResponse);
    } else {
      this.typewriterEffect(aiChatbotResponse);
    }
  }

  /**
   * Handles SQL query responses by displaying them as formatted tables with followup questions.
   * @param aiChatbotResponse - The AI response containing SQL query results
   */
  private handleSqlResponse(aiChatbotResponse: AIChatbotResponseDTO): void {
    const messages = this.messages();
    const lastMessageIndex = messages.length - 1;

    this.messages.update((msgs) => {
      const updatedMsgs = [...msgs];
      updatedMsgs[lastMessageIndex] = {
        content: aiChatbotResponse.aiResponseData,
        isBot: true,
        isTyping: false,
        contentType: 'markdown-table',
        userIntent: aiChatbotResponse.userIntent,
        followupQuestions: aiChatbotResponse.followupQuestions || [],
      };
      return updatedMsgs;
    });

    this.isProcessing.set(false);
    this.shouldScrollToBottom = true;
  }

  /**
   * Displays AI responses with a typewriter animation effect for enhanced user experience.
   * @param aiChatbotResponse - The AI response to display with typewriter animation
   */
  private typewriterEffect(aiChatbotResponse: AIChatbotResponseDTO): void {
    const fullText = aiChatbotResponse.aiResponseData;
    const messages = this.messages();
    const lastMessageIndex = messages.length - 1;

    this.messages.update((msgs) => {
      const updatedMsgs = [...msgs];
      updatedMsgs[lastMessageIndex] = {
        content: '',
        isBot: true,
        isTyping: false,
        contentType: 'markdown',
        userIntent: aiChatbotResponse.userIntent,
        followupQuestions: aiChatbotResponse.followupQuestions || [],
      };
      return updatedMsgs;
    });

    let currentIndex = 0;
    const typeSpeed = 5;

    const typeInterval = setInterval(() => {
      if (currentIndex < fullText.length) {
        const currentText = fullText.substring(0, currentIndex + 1);

        this.messages.update((msgs) => {
          const updatedMsgs = [...msgs];
          updatedMsgs[lastMessageIndex] = {
            content: currentText,
            isBot: true,
            isTyping: false,
            contentType: 'markdown',
            userIntent: aiChatbotResponse.userIntent,
            followupQuestions: aiChatbotResponse.followupQuestions || [],
          };
          return updatedMsgs;
        });

        currentIndex++;
        this.shouldScrollToBottom = true;
      } else {
        this.isProcessing.set(false);
        clearInterval(typeInterval);
        this.shouldScrollToBottom = true;
      }
    }, typeSpeed);
  }

  // #endregion
}
