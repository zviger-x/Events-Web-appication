import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../App";
import EventsForm from "../Components/Events/EventsForm";
import EventsTable from "../Components/Events/EventsTable";

export const Routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '*', element: <EventsTable /> },
            { path: 'createEvent', element: <EventsForm key='create' /> },
            { path: 'event/:id', element: <EventsForm key='details' /> },
            { path: 'editEvent/:id', element: <EventsForm key='edit' /> },
            { path: 'deleteEvent/:id', element: <EventsForm key='delete' /> },
        ]
    }
]

export const Router = createBrowserRouter(Routes);