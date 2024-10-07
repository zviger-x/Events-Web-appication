import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../App";
import EventsTable from "../Components/Events/EventsTable";
import EventCreateForm from "../Components/Events/EventCreateForm";
import EventDetailsForm from "../Components/Events/EventDetailsForm";
import EventEditForm from "../Components/Events/EventEditForm";
import EventDeleteForm from "../Components/Events/EventDeleteForm";
import PrivateRoute from "./PrivateRoutes";
import UserRoles from "../Models/Users/UserRoles";
import AuthorizedRoute from "./AuthorizedRoutes";
import RegisterForm from "../Components/User/RegisterForm";
import LoginForm from "../Components/User/LoginForm";
import UserEditForm from "../Components/User/UserEditForm";
import UserDetailsForm from "../Components/User/UserDetailsForm";

export const Routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '*', element: <EventsTable /> },
            { path: 'events', element: <EventsTable /> },
            { path: 'event/:id', element: <EventDetailsForm /> },
            { path: 'event/create', element: (
                <PrivateRoute requiredRole={UserRoles.Admin}>
                    <EventCreateForm />
                </PrivateRoute>
                ) },
            { path: 'event/:id/edit', element: (
                    <PrivateRoute requiredRole={UserRoles.Admin}>
                        <EventEditForm />
                    </PrivateRoute>
                ) },
            { path: 'event/:id/delete', element: (
                    <PrivateRoute requiredRole={UserRoles.Admin}>
                        <EventDeleteForm />
                    </PrivateRoute>
                ) },
            {
                path: 'account/login', element: (
                    <AuthorizedRoute needAuthorization={false}>
                        <LoginForm />
                    </AuthorizedRoute>
                )
            },
            {
                path: 'account/register', element: (
                    <AuthorizedRoute needAuthorization={false}>
                        <RegisterForm />
                    </AuthorizedRoute>
                )
            },
            {
                path: 'account/:id', element: (
                    <AuthorizedRoute needAuthorization={true}>
                        <UserDetailsForm />
                    </AuthorizedRoute>
                )
            },
            {
                path: 'account/:id/edit', element: (
                    <AuthorizedRoute needAuthorization={true}>
                        <UserEditForm />
                    </AuthorizedRoute>
                )
            },
        ]
    }
]

export const Router = createBrowserRouter(Routes);