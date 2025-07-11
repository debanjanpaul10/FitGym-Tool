import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';
import { filter, take } from 'rxjs';

export function msalInitializer(
  msalService: MsalService,
  broadcastService: MsalBroadcastService
): () => Promise<void> {
  return () => {
    return new Promise<void>((resolve) => {
      let resolved = false;
      const timeout = setTimeout(() => {
        if (!resolved) {
          resolved = true;
          const accounts = msalService.instance.getAllAccounts();
          if (accounts.length > 0) {
            msalService.instance.setActiveAccount(accounts[0]);
          }

          resolve();
        }
      }, 5000);

      msalService.handleRedirectObservable().subscribe({
        next: (result) => {
          if (result) {
            msalService.instance.setActiveAccount(result.account);
          }
        },
        error: (error) => console.error('Redirect error', error),
        complete: () => {
          broadcastService.inProgress$
            .pipe(
              filter((status: InteractionStatus) => {
                return status === InteractionStatus.None;
              }),
              take(1)
            )
            .subscribe(() => {
              if (!resolved) {
                resolved = true;
                clearTimeout(timeout);

                const accounts = msalService.instance.getAllAccounts();
                if (accounts.length > 0) {
                  msalService.instance.setActiveAccount(accounts[0]);
                }
                resolve();
              }
            });
        },
      });
    });
  };
}
