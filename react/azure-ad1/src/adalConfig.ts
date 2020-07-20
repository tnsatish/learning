import { AuthenticationContext, AdalConfig } from 'react-adal';

export const adalConfig: AdalConfig = {
    tenant: '',
    clientId: '',
    redirectUri: 'http://localhost:3000/',
    endpoints: {
        api: 'http://localhost:44307/',
    },
    cacheLocation: 'sessionStorage'
};

export const authContext = new AuthenticationContext(adalConfig);

export const getToken = () => authContext.getCachedToken(adalConfig.clientId);