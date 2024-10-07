import { useEffect, useState } from "react";
import { NavLink, useSearchParams } from "react-router-dom";
import { Button, Container, Dropdown, Form } from "semantic-ui-react";
import APIConnector from "../../API/APIConnector";
import TH from '../../API/TokenHandler';
import { EventDTO } from "../../Models/Events/EventDTO";
import UserRoles from "../../Models/Users/UserRoles";
import "./EventsTable.css";
import EventsTableItem from "./EventsTableItem";

export default function EventsTable() {
    const userRole = String(TH.GetUserRole(TH.ParseToken(TH.GetToken()!)));
    const [events, setEvents] = useState<EventDTO[]>();
    const [searchParams, setSearchParams] = useSearchParams();
    const [selectedSort, setSelectedSort] = useState<string | undefined>();
    const [filterValue, setFilterValue] = useState<string | undefined>();

    const sortBy = searchParams.get("sortBy") || undefined;
    const value = searchParams.get("value") || undefined;
    const page = searchParams.get("page") || "1";

    useEffect(() => {
        if (parseInt(page) <= 0) handlePageChange(1);

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

            params.set("page", newPage.toString());
            setSearchParams(params);
        }
    };

    const handleSortChange = (sortBy?: string, value?: string) => {
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

    const handleApplyFilter = () => {
        handleSortChange(selectedSort || undefined, filterValue || undefined);
    };

    if (!events) {
        return <h1 className="load-text">Loading...</h1>;
    }

    return (
        <>
            <Container className="container-style">
                <div className="table-upper-name">
                    <h1 style={{ margin: '0' }}>Events table</h1>
                </div>

                <Form className="filters" style={{ padding: '15px 0px 0px 0px' }}>
                    <div className="container">
                        <Form.Field style={{ padding: '0px 10px 0px 0px' }}>
                            <label>Sort by</label>
                            <Dropdown
                                placeholder='Sort by'
                                selection
                                options={[
                                    { key: 'none', text: 'None', value: undefined },
                                    { key: 'name', text: 'Name', value: 'name' },
                                    { key: 'category', text: 'Category', value: 'category' },
                                    { key: 'venue', text: 'Venue', value: 'venue' },
                                    { key: 'date', text: 'Date', value: 'date' },
                                ]}
                                onChange={(e, { value }) => setSelectedSort(value as string)}
                            />
                        </Form.Field>
                        <Form.Field style={{ padding: '0px 10px 0px 0px' }}>
                            <label>Filter</label>
                            <input
                                type="text"
                                placeholder="Filter value"
                                onChange={(e) => setFilterValue(e.target.value)}
                            />
                        </Form.Field>

                    </div>
                    <Button onClick={handleApplyFilter}>Apply filter</Button>
                </Form>

                {userRole && userRole === UserRoles.Admin && (
                    <Button as={NavLink} to="event/create" floated="right" type="button" content="Create event" positive style={{ margin: '0px 0px 15px 0px' }} />
                )}

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
                        {events.length > 0 && events.map((event, index) => (
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