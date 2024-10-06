import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../App";
import EventsTable from "../Components/Events/EventsTable";
import EventCreateForm from "../Components/Events/EventCreateForm";
import EventDetailsForm from "../Components/Events/EventDetailsForm";
import EventEditForm from "../Components/Events/EventEditForm";
import EventDeleteForm from "../Components/Events/EventDeleteForm";

export const Routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '*', element: <EventsTable /> },
            { path: 'events', element: <EventsTable /> },
            { path: 'event/create', element: <EventCreateForm /> },
            { path: 'event/:id', element: <EventDetailsForm /> },
            { path: 'event/:id/edit', element: <EventEditForm /> },
            { path: 'event/:id/delete', element: <EventDeleteForm /> },
        ]
    }
]

export const Router = createBrowserRouter(Routes);