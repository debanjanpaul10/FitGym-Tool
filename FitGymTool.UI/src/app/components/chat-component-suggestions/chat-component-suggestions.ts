import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SuggestionItem } from '@models/interfaces/suggestion-item.interface';

@Component({
  selector: 'app-chat-component-suggestions',
  imports: [CommonModule],
  templateUrl: './chat-component-suggestions.html',
  styleUrl: './chat-component-suggestions.scss',
})
export class ChatComponentSuggestions {
  @Input() suggestions: SuggestionItem[] = [];
  @Input() title: string = '';
  @Input() type: 'sample-prompts' | 'followup-questions' = 'sample-prompts';
  @Input() maxVisible: number = 6;
  @Input() showIcons: boolean = true;
  @Input() layout: 'grid' | 'list' = 'grid';

  @Output() suggestionSelected = new EventEmitter<SuggestionItem>();
  @Output() showMoreClicked = new EventEmitter<void>();

  get visibleSuggestions(): SuggestionItem[] {
    return this.suggestions.slice(0, this.maxVisible);
  }

  get hasMoreSuggestions(): boolean {
    return this.suggestions.length > this.maxVisible;
  }

  protected onSuggestionClick(suggestion: SuggestionItem): void {
    this.suggestionSelected.emit(suggestion);
  }

  protected onShowMore(): void {
    this.showMoreClicked.emit();
  }

  protected trackBySuggestion(
    index: number | string | undefined,
    suggestion: SuggestionItem
  ): string {
    return suggestion.id || suggestion.text;
  }
}
