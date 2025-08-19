import { Component, inject, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DockModule } from 'primeng/dock';
import { TooltipModule } from 'primeng/tooltip';
import { ButtonModule } from 'primeng/button';

import { ToasterService } from '@core/services/toaster.service';
import { AiApiService } from '@services/ai-services-api.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ChatMessageRequestDTO } from '@models/DTO/chat-message-request-dto.model';
import { CommonApplicationConstants } from '@shared/application.constants';

@Component({
  selector: 'app-chat-component',
  standalone: true,
  imports: [CommonModule, DockModule, TooltipModule, ButtonModule],
  templateUrl: './chat-component.html',
  styleUrl: './chat-component.scss',
})
export class ChatComponent {
  protected isChatOpen: WritableSignal<boolean> = signal(false);
  protected isProcessing: WritableSignal<boolean> = signal(false);
  protected AIMessages = CommonApplicationConstants.AIConstants;
  protected messages: WritableSignal<
    Array<{ content: string; isBot: boolean; isTyping?: boolean }>
  > = signal([
    {
      content: this.AIMessages.AiGreetingMessage,
      isBot: true,
    },
  ]);

  private readonly _toasterService: ToasterService = inject(ToasterService);
  private readonly _aiApiService: AiApiService = inject(AiApiService);

  protected toggleChat(): void {
    this.isChatOpen.set(!this.isChatOpen());
  }

  protected closeChat(): void {
    this.isChatOpen.set(false);
  }

  protected refreshChats(): void {
    this.messages.set([
      {
        content: this.AIMessages.AiGreetingMessage,
        isBot: true,
      },
    ]);
    this.isProcessing.set(false);
  }

  protected sendMessage(event: any): void {
    const input =
      event.target.tagName === 'INPUT'
        ? event.target
        : event.target.previousElementSibling;
    const message = input.value.trim();

    if (message && !this.isProcessing()) {
      this.isProcessing.set(true);
      this.messages.update((msgs) => [
        ...msgs,
        { content: message, isBot: false },
      ]);
      input.value = '';

      this.messages.update((msgs) => [
        ...msgs,
        { content: '', isBot: true, isTyping: true },
      ]);

      const request: ChatMessageRequestDTO = {
        chatMessage: message,
      };
      this.sendAiMessageToApiAsync(request);
    }
  }

  private sendAiMessageToApiAsync(request: ChatMessageRequestDTO): void {
    this._aiApiService.RespondAsync(request).subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.typewriterEffect(response.responseData);
        } else {
          this.handleErrorResponse(this.AIMessages.AIFailedMessage);
        }
      },
      error: (error: any) => {
        console.error(error);
        this.removeTypingIndicator();

        let errorMessage = this.AIMessages.SendMessageFailed;
        if (error?.error?.errors?.chatMessage) {
          errorMessage = error.error.errors.chatMessage[0];
        } else if (error?.message) {
          errorMessage = error.message;
        }

        this._toasterService.showError(errorMessage);
      },
    });
  }

  private handleErrorResponse(errorMessage: string): void {
    this.removeTypingIndicator();
    this.isProcessing.set(false);
    this._toasterService.showError(errorMessage);
  }

  private removeTypingIndicator(): void {
    this.messages.update((msgs) => {
      const filteredMsgs = msgs.filter((msg) => !msg.isTyping);
      return filteredMsgs;
    });
    this.isProcessing.set(false);
  }

  private typewriterEffect(fullText: string): void {
    const messages = this.messages();
    const lastMessageIndex = messages.length - 1;

    this.messages.update((msgs) => {
      const updatedMsgs = [...msgs];
      updatedMsgs[lastMessageIndex] = {
        content: '',
        isBot: true,
        isTyping: false,
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
          };
          return updatedMsgs;
        });

        currentIndex++;
      } else {
        this.isProcessing.set(false);
        clearInterval(typeInterval);
      }
    }, typeSpeed);
  }
}
