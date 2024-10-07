const TokenHandler = {
    GetToken: () => {
        return localStorage.getItem('token');
    },

    ParseToken: (token: string) => {
        if (!token) return null;
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(escape(window.atob(base64)));
        return JSON.parse(jsonPayload);
    },

    GetUserRole: (decoded: any) => {
        return decoded ? decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] : null;
    },

    GetUserName: (decoded: any) => {
        return decoded ? decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] : null;
    },

    GetUserEmail: (decoded: any) => {
        return decoded ? decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] : null;
    },

    GetUserId: (decoded: any) => {
        return decoded ? decoded["Id"] : null;
    },

    GetAllClaims: (decoded: any) => {
        if (!decoded) return null;

        return {
            name: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || null,
            email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] || null,
            role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null,
            id: decoded["Id"] || null,
        };
    }
};

export default TokenHandler;