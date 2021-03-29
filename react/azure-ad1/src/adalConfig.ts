import { AuthenticationContext, AdalConfig } from 'react-adal';

export const adalConfig: AdalConfig = {
    tenant: '48e712aa-5a0a-4b80-892a-df758101d104',
    clientId: '60c6175c-5909-44cb-b228-4105dd16f939',
    redirectUri: 'http://localhost:3002/',
    endpoints: {
        api: 'http://localhost:5000/',
    },
    cacheLocation: 'sessionStorage'
};

export const authContext = new AuthenticationContext(adalConfig);

export const getToken = () => authContext.getCachedToken(adalConfig.clientId);
