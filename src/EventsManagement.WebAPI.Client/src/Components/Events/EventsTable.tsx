import { useEffect, useState } from "react";
import { EventDTO } from "../../Models/Events/EventDTO";
import APIConnector from "../../API/APIConnector";
import { Button, Container } from "semantic-ui-react";
import EventsTableItem from "./EventsTableItem";
import { NavLink, useSearchParams } from "react-router-dom";
import "./EventsTable.css";

export default function EventsTable() {
    const [events, setEvents] = useState<EventDTO[]>();
    const [searchParams, setSearchParams] = useSearchParams();

    const sortBy = searchParams.get("sortBy") || undefined;
    const value = searchParams.get("value") || undefined;
    const page = searchParams.get("page") || "1";

    useEffect(() => {
        if (parseInt(page) <= 0)
            handlePageChange(1);

        const fetchData = async () => {
            const fetchedEvents = await APIConnector.GetEvents(sortBy, value, page);
            setEvents(fetchedEvents);
        };

        fetchData();
    }, [sortBy, value, page]);

    const handlePageChange = async (newPage: number) => {
        if (newPage <= 0) newPage = 1;

        const fetchedEvents = await APIConnector.GetEvents(sortBy, value, newPage.toString());
        if (fetchedEvents.length > 0) {
            const params = new URLSearchParams(searchParams.toString());
            
            if (newPage) {
                params.set("page", newPage.toString());
            } else {
                params.delete("page");
            }

            setSearchParams(params);
        }
    };

    const handleSortChange = (sortBy: string, value: string) => {
        const params = new URLSearchParams(searchParams.toString());

        if (sortBy) {
            params.set("sortBy", sortBy);
        } else {
            params.delete("sortBy");
        }

        if (value) {
            params.set("value", value);
        } else {
            params.delete("value");
        }

        setSearchParams(params);
    };

    if (!events) {
        return <h1 className="load-text">Loading...</h1>;
    }

    return (
        <>
            <Container className="container-style">
                <div className="table-upper-name">
                    <h1 style={{ margin: '0' }}>Events table</h1>
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
                        {events.map((event, index) => (
                            <EventsTableItem key={index} event={event} />
                        ))}
                    </tbody>
                </table>
                {page && (() => {
                    const pn = parseInt(page);
                    if (isNaN(pn) || pn < 1) {
                        return null;
                    }
                    return (
                        <div className="pagination">
                            <Button onClick={() => handlePageChange(pn - 1)}><label>{'<'}</label></Button>
                            <span>{pn}</span>
                            <Button onClick={() => handlePageChange(pn + 1)}><label>{'>'}</label></Button>
                        </div>
                    );
                })()}
            </Container>
        </>
    );
}