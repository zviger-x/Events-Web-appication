import { useEffect, useState } from "react";
import { useParams, NavLink } from "react-router-dom";
import { UserDTO } from "../../Models/Users/UserDTO";
import { EventDTO } from "../../Models/Events/EventDTO"; // Импортируйте EventDTO
import APIConnector from "../../API/APIConnector";
import { Button, Segment } from "semantic-ui-react";
import "./UserLRForm.css";
import TH from "../../API/TokenHandler"
import UserRoles from "../../Models/Users/UserRoles";
import EventsTableItem from "../Events/EventsTableItem"; 

export default function UserDetailsForm() {
    const userRole = String(TH.GetUserRole(TH.ParseToken(TH.GetToken()!)));
    const { id } = useParams();
    const [user, setUser] = useState<UserDTO>();
    const [events, setEvents] = useState<EventDTO[]>();

    useEffect(() => {
        if (id) {
            const fetchData = async () => {
                const fetchedUser = await APIConnector.GetUserById(parseInt(id));
                setUser(fetchedUser);

                const fetchedEvents = await APIConnector.GetUserEvents(parseInt(id));
                setEvents(fetchedEvents);
            }

            fetchData();
        }
    }, [id]);

    if (!user) {
        return <h1 className="load-text">Loading...</h1>;
    }

    const eventDate = new Date(user.birthDate!);
    const formattedDate = eventDate.toLocaleDateString('ru-RU', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
    });

    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Segment className="form-container" style={{ display: 'inline-block', width: 'auto' }}>
                <div className="event-info-container">
                    <div className="event-details">
                        <div style={{ padding: '6px' }}>
                            <h1>Your profile</h1>
                        </div>
                        <table>
                            <tbody>
                                <tr>
                                    <td><strong>Name:</strong></td>
                                    <td>{user.surname} {user.name}</td>
                                </tr>
                                <tr>
                                    <td><strong>Birth date:</strong></td>
                                    <td>{formattedDate}</td>
                                </tr>
                                <tr>
                                    <td><strong>Email:</strong></td>
                                    <td>{user.email}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div className="event-buttons">
                    {userRole && userRole === UserRoles.Admin && (
                        <>
                            <Button as={NavLink} to={`delete`} floated='right' color="red" type="submit">Delete</Button>
                            <Button as={NavLink} to={`edit`} floated='right' color="yellow" type="submit">Edit</Button>
                        </>
                    )}
                    <Button as={NavLink} to='/' floated='right' type='button' content='Back' />
                </div>
                {(events && events?.length > 0) && (
                    <div className="user-events">
                        <h1>Events you are a participant in:</h1>
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
                    </div>
                )}
            </Segment>
        </div>
    );
}