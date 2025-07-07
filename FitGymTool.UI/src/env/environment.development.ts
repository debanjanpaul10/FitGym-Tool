export const environment = {
  production: false,
  apiBaseUrl: 'https://localhost:6969',
  msalConfig: {
    auth: {
      clientId: 'ef5fa0ef-66c4-4a96-b2f6-974ee5e0ab7b',
      authority:
        'https://FitGymTool.ciamlogin.com/2b416779-fe70-4a4c-bbac-9a4a75588ab8',
    },
    scopes: ['User.Read', 'Users.Write'],
  },
  apiConfig: {
    scopes: [
      'api://6dfe8fda-388a-4896-9bb2-2482ac4da8bf/User.Read',
      'api://6dfe8fda-388a-4896-9bb2-2482ac4da8bf/Users.Write',
    ],
    uri: 'https://graph.microsoft.com/v1.0/me',
    apiScope: ['api://6dfe8fda-388a-4896-9bb2-2482ac4da8bf/Users.Write'],
  },
  idleConfig: {
    idleTimeoutMinutes: 30,
    timeoutWarningTimeMinutes: 30,
  },
};
