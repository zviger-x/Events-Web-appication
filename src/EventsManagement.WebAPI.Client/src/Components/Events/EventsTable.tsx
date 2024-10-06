import { useEffect, useState } from "react"
import { EventDTO } from "../../Models/Events/EventDTO"
import APIConnector from "../../API/APIConnector";
import { Button, Container } from "semantic-ui-react";
import EventsTableItem from "./EventsTableItem";
import { NavLink } from "react-router-dom";

export default function EventsTable() {

    const [events, setEvents] = useState<EventDTO[]>();

    useEffect(() => {
        const fetchData = async () => {
            const fetchedEvents = await APIConnector.GetEvents();
            setEvents(fetchedEvents);
        }

        fetchData();
    }, []);

    if (!events) {
        return <h1 className="load-text">Loading...</h1>;
    }

    if (events.length == 0) {
        return (
            <h1 className="load-text no-events-info-container">No events
                <Button as={NavLink} to="event/create" type="button" content="Create event" positive />
            </h1>
        );
    }

    return (
        <>
            <Container className="container-style">
                <div className="table-upper-name">
                    <h1 style={{margin: '0'}}>Events table</h1>
                    <Button as={NavLink} to="event/create" floated="right" type="button" content="Create event" positive />
                </div>
                <table className="ui table">
                    <thead style={{ textAlign: 'center' }}>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Date and time</th>
                            <th>Venue</th>
                            <th>Category</th>
                            <th>Participants</th>
                            <th>Image</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {events.length !== 0 && (
                            events.map((event, index) => (
                                <EventsTableItem key={index} event={event} />
                            ))
                        )}
                    </tbody>
                </table>
            </Container>
        </>
    )
}