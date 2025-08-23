export const environment = {
  production: false,
  apiBaseUrl: 'https://localhost:6969',
  msalConfig: {
    auth: {
      clientId: '9f4124f9-5ec7-4084-866b-d42f3e82b02f',
      authority: 'https://login.microsoftonline.com/common/',
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
