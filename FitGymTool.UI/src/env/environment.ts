export const environment = {
  production: true,
  apiBaseUrl:
    'https://app-webapi-fitgym-tool-hegdfmgnb0gdgva7.centralindia-01.azurewebsites.net',
  msalConfig: {
    auth: {
      clientId: 'a60b28a6-c429-47e7-bdb4-19502849f14e',
      authority: 'https://login.microsoftonline.com/consumers/',
    },
    scopes: ['Users.Read', 'Users.Write'],
  },
  apiConfig: {
    scopes: [
      'api://0c49dfb9-3afe-4f24-934f-340be62b8cbd/Users.Read',
      'api://0c49dfb9-3afe-4f24-934f-340be62b8cbd/Users.Write',
    ],
    uri: 'https://graph.microsoft.com/v1.0/me',
    apiScope: ['api://0c49dfb9-3afe-4f24-934f-340be62b8cbd/Users.Write'],
  },
  idleConfig: {
    idleTimeoutMinutes: 30,
    timeoutWarningTimeMinutes: 30,
  },
};
