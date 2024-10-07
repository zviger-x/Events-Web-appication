import { Navigate } from 'react-router-dom';
import TH from "../API/TokenHandler";

interface PrivateRouteProps {
    children: JSX.Element;
    needAuthorization: boolean;
}

// if needAuthorization false -> ignore all authorization
const AuthorizedRoute = ({ children, needAuthorization }: PrivateRouteProps) => {
    const userRole = TH.GetUserRole(TH.ParseToken(TH.GetToken()!));

    if (needAuthorization) {
        if (!userRole) return <Navigate to="/account/login" replace />;
        else return children;
    }
    else if (!userRole) return children;
};

export default AuthorizedRoute;