import {
  Component,
  computed,
  inject,
  OnDestroy,
  OnInit,
  signal,
} from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AccountInfo } from '@azure/msal-browser';
import { Ripple } from 'primeng/ripple';

import { Utilities } from '@core/helpers/utilities-helper';

@Component({
  selector: 'app-current-user-component',
  standalone: true,
  imports: [Ripple],
  templateUrl: './current-user.component.html',
  styleUrl: './current-user.component.scss',
})
/**
 * @component CurrentUserComponent
 * A dashboard component that displays the current authenticated user's information,
 * personalized greeting, and real-time date/time. Integrates with Microsoft Authentication Library (MSAL)
 * to retrieve and display user account details.
 */
export class CurrentUserComponent implements OnInit, OnDestroy {
  private readonly _msalService = inject(MsalService);
  private _timeInterval?: number;

  protected readonly currentUserProfile = signal<AccountInfo | null>(null);
  protected readonly currentDateTime = signal<Date>(new Date());

  protected readonly currentUserName = computed(
    () => this.currentUserProfile()?.name ?? ''
  );
  protected readonly greetingText = computed(() => Utilities.GetGreeting());
  protected readonly formattedDateTime = computed(() => {
    const date = this.currentDateTime();
    return (
      date.toLocaleDateString('en-US', {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      }) +
      ' • ' +
      date.toLocaleTimeString('en-US', {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
      })
    );
  });

  ngOnInit(): void {
    const activeAccount = this._msalService.instance.getActiveAccount();
    this.currentUserProfile.set(activeAccount);

    this._timeInterval = window.setInterval(() => {
      this.currentDateTime.set(new Date());
    }, 1000);
  }

  ngOnDestroy(): void {
    if (this._timeInterval) {
      clearInterval(this._timeInterval);
    }
  }
}
