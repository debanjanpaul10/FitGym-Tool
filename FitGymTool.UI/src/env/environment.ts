export const environment = {
  production: true,
  apiBaseUrl:
    'https://app-webapi-fitgym-tool-hegdfmgnb0gdgva7.centralindia-01.azurewebsites.net',
  msalConfig: {
    auth: {
      clientId: '9f4124f9-5ec7-4084-866b-d42f3e82b02f',
      authority: 'https://login.microsoftonline.com/consumer/',
    },
    scopes: ['Users.Read', 'Users.Write'],
  },
  apiConfig: {
    scopes: [
      'api://bdb9b6f0-8229-4911-8390-8efc1fee5373/Users.Read',
      'api://bdb9b6f0-8229-4911-8390-8efc1fee5373/Users.Write',
    ],
    uri: 'https://graph.microsoft.com/v1.0/me',
    apiScope: ['api://bdb9b6f0-8229-4911-8390-8efc1fee5373/Users.Write'],
  },
  idleConfig: {
    idleTimeoutMinutes: 30,
    timeoutWarningTimeMinutes: 30,
  },
};
