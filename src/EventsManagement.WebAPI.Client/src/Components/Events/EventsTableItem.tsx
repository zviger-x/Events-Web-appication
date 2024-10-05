import { Button } from "semantic-ui-react";
import { EventDTO } from "../../Models/Events/EventDTO";
import APIConnector from "../../API/APIConnector";
import noImg from "../../assets/no_img.jpg";

interface Props {
    event: EventDTO;
}

export default function EventsTableItem({ event }: Props) {
    const eventDate = new Date(event.dateAndTime);

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

    return (
        <tr className="center aligned">
            <td data-label="Name">{event.name}</td>
            <td data-label="Description">{event.description}</td>
            <td data-label="DateAndTime">{`${formattedTime}, ${formattedDate}`}</td>
            <td data-label="Venue">{event.venue}</td>
            <td data-label="Category">{event.category}</td>
            <td data-label="Participants">{event.currentParticipants}/{event.maxNumberOfParticipants}</td>
            <td data-label="Image">
                {imageSrc && <img src={imageSrc} alt="Event" style={{ maxHeight: '100px' }} />}
            </td>
            <td data-label="Action">
                <Button color="yellow" type="submit">Edit</Button>
                <Button type="button" negative onClick={async () => { APIConnector.DeleteEvent(event.id); /* window.location.reload();*/ }}>Delete</Button>
            </td>
        </tr>
    )
}