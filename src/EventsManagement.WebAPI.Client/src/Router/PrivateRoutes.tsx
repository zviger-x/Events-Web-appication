import { Navigate } from 'react-router-dom';
import TH from "../API/TokenHandler";

interface PrivateRouteProps {
    children: JSX.Element;
    requiredRole: string | null;
}

const PrivateRoute = ({ children, requiredRole }: PrivateRouteProps) => {
    const userRole = TH.GetUserRole(TH.ParseToken(TH.GetToken()!));

    if (!userRole)
        return <Navigate to="account/login" replace />;

    if (userRole !== requiredRole)
        return <Navigate to="/" replace />;

    return children;
};

export default PrivateRoute;