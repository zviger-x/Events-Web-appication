import { useEffect, useState } from "react";
import { useParams, NavLink, useNavigate } from "react-router-dom";
import { EventDTO } from "../../Models/Events/EventDTO";
import APIConnector from "../../API/APIConnector";
import { Button, Segment } from "semantic-ui-react";
import noImg from "../../assets/no_img.jpg";
import "./EventDetails.css";
import TH from "../../API/TokenHandler";
import UserRoles from "../../Models/Users/UserRoles";

export default function EventsDetailsForm() {
    const userRole = TH.GetUserRole(TH.ParseToken(TH.GetToken()!));
    const { id } = useParams();
    const [event, setEvent] = useState<EventDTO>();
    const [isRegistered, setIsRegistered] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchData = async () => {
            if (id) {
                const fetchedEvent = await APIConnector.GetEventById(parseInt(id));
                setEvent(fetchedEvent);

                const userId = TH.GetUserId(TH.ParseToken(TH.GetToken()!));
                const registered = await APIConnector.IsUserRegisteredForEvent(userId, id);
                setIsRegistered(registered);
            }
        };

        fetchData();
    }, [id]);

    if (!event) {
        return <h1 className="load-text">Loading...</h1>;
    }

    const eventDate = new Date(event.dateAndTime!);
    const formattedTime = eventDate.toLocaleTimeString('ru-RU', {
        hour: '2-digit',
        minute: '2-digit',
    });
    const formattedDate = eventDate.toLocaleDateString('ru-RU', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
    });

    const imageSrc = event.image ? `data:image;base64,${event.image}` : noImg;

    const handleRegister = async () => {
        const userId = TH.GetUserId(TH.ParseToken(TH.GetToken()!));
        await APIConnector.RegisterForEvent(userId, id!);
        navigate(`/account/${userId}`);
    };

    const handleUnregister = async () => {
        const userId = TH.GetUserId(TH.ParseToken(TH.GetToken()!));
        await APIConnector.UnregisterFromEvent(userId, id!);
        navigate(`/account/${userId}`);
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Segment className="form-container" style={{ display: 'inline-block', width: 'auto' }}>
                <div className="event-info-container">
                    <div className="event-image">
                        {imageSrc && <img src={imageSrc} alt="Event" style={{ maxHeight: '100px' }} />}
                    </div>
                    <div className="event-details">
                        <div style={{ padding: '6px' }}>
                            <h1>Event Information</h1>
                        </div>
                        <table>
                            <tbody>
                                <tr>
                                    <td><strong>Name:</strong></td>
                                    <td>{event.name}</td>
                                </tr>
                                <tr>
                                    <td><strong>Description:</strong></td>
                                    <td>{event.description}</td>
                                </tr>
                                <tr>
                                    <td><strong>Date and Time:</strong></td>
                                    <td>{`${formattedTime}, ${formattedDate}`}</td>
                                </tr>
                                <tr>
                                    <td><strong>Venue:</strong></td>
                                    <td>{event.venue}</td>
                                </tr>
                                <tr>
                                    <td><strong>Category:</strong></td>
                                    <td>{event.category}</td>
                                </tr>
                                <tr>
                                    <td><strong>Number of Participants:</strong></td>
                                    <td>{event.currentNumberOfParticipants}/{event.maxNumberOfParticipants}</td>
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
                    {(userRole && event.currentNumberOfParticipants < event.maxNumberOfParticipants) && (isRegistered ? (
                        <Button floated='right' color="orange" onClick={handleUnregister}>Unregister from event</Button>
                    ) : (
                        <Button floated='right' color="green" onClick={handleRegister}>Register to event</Button>
                    ))}
                    <Button as={NavLink} to='/' floated='right' type='button' content='Back' />
                </div>
            </Segment>
        </div>
    );
}
