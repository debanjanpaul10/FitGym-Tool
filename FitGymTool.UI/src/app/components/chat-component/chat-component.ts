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

@Component({
  selector: 'app-chat-component',
  standalone: true,
  imports: [CommonModule, DockModule, TooltipModule, ButtonModule],
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

  ngAfterViewChecked(): void {
    if (this.shouldScrollToBottom) {
      this.scrollToBottom();
    }
  }

  protected onMessagesScroll(): void {
    if (this.messagesContainer) {
      const element = this.messagesContainer.nativeElement;
      const isAtBottom =
        element.scrollHeight - element.scrollTop <= element.clientHeight + 10;
      this.shouldScrollToBottom = isAtBottom;
      this.showScrollToBottomButton = !isAtBottom;
    }
  }

  protected scrollToBottomManually(): void {
    this.shouldScrollToBottom = true;
    this.scrollToBottom();
    this.showScrollToBottomButton = false;
  }

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

  protected toggleChat(): void {
    this.isChatOpen.set(!this.isChatOpen());
    setTimeout(() => {
      this.shouldScrollToBottom = true;
      this.scrollToBottom();
    }, 100);
  }

  protected closeChat(): void {
    this.isChatOpen.set(false);
    this.isExpanded.set(false);
  }

  protected toggleExpand(): void {
    this.isExpanded.set(!this.isExpanded());
    setTimeout(() => {
      this.shouldScrollToBottom = true;
      this.scrollToBottom();
    }, 100);
  }

  protected refreshChats(): void {
    this.loadUserProfile();
    this.initializeMessages();
    this.isProcessing.set(false);
    this.shouldScrollToBottom = true;
  }

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

  protected insertSamplePrompt(promptText: string): void {
    if (this.messageInput && !this.isProcessing()) {
      this.messageInput.nativeElement.value = promptText;
      this.messageInput.nativeElement.focus();
    }
  }

  // #region PRIVATE METHODS

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

        this.showErrorAsAiResponse(errorMessage);
        this._toasterService.showError(errorMessage);
      },
    });
  }

  private handleErrorResponse(errorMessage: string): void {
    this.showErrorAsAiResponse(errorMessage);
    this._toasterService.showError(errorMessage);
  }

  private showErrorAsAiResponse(errorMessage: string): void {
    const messages = this.messages();
    const lastMessageIndex = messages.length - 1;

    if (messages[lastMessageIndex]?.isTyping) {
      this.messages.update((msgs) => {
        const updatedMsgs = [...msgs];
        updatedMsgs[lastMessageIndex] = {
          content: `Error: ${errorMessage}`,
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
          content: `Error: ${errorMessage}`,
          isBot: true,
          isTyping: false,
          contentType: 'text',
        },
      ]);
    }

    this.isProcessing.set(false);
    this.shouldScrollToBottom = true;
  }

  private handleAiResponse(aiChatbotResponse: AIChatbotResponseDTO): void {
    if (aiChatbotResponse.userIntent === 'SQL') {
      this.handleSqlResponse(aiChatbotResponse);
    } else {
      this.typewriterEffect(aiChatbotResponse);
    }
  }

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
      };
      return updatedMsgs;
    });

    this.isProcessing.set(false);
    this.shouldScrollToBottom = true;
  }

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
        contentType: 'text',
        userIntent: aiChatbotResponse.userIntent,
      };
      return updatedMsgs;
    });

    let currentIndex = 0;
    const typeSpeed = 50;

    const typeInterval = setInterval(() => {
      if (currentIndex < fullText.length) {
        const currentText = fullText.substring(0, currentIndex + 1);

        this.messages.update((msgs) => {
          const updatedMsgs = [...msgs];
          updatedMsgs[lastMessageIndex] = {
            content: currentText,
            isBot: true,
            isTyping: false,
            contentType: 'text',
            userIntent: aiChatbotResponse.userIntent,
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
