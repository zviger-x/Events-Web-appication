import { ChangeEvent, useEffect, useState } from "react";
import { NavLink, useNavigate, useParams } from "react-router-dom"
import { EventDTO } from "../../Models/Events/EventDTO";
import APIConnector from "../../API/APIConnector";
import { Button, Form, Segment } from "semantic-ui-react";

export default function EventsForm() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [evt, setEvent] = useState<EventDTO>({
        id: undefined,
        name: '',
        description: '',
        dateAndTime: undefined,
        venue: '',
        category: '',
        currentNumberOfParticipants: 0,
        maxNumberOfParticipants: 0,
        image: '',
    });

    useEffect(() => {
        if (id) {
            APIConnector.GetEventById(parseInt(id)).then(evt => setEvent(evt!));
        }
    }, [id]);

    function handleSubmit() {
        if (!evt.id) {
            APIConnector.CreateEvent(evt).then(() => navigate('/'));
        } else {
            APIConnector.EditEvent(evt).then(() => navigate('/'));
        }
    }

    function handleImputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target;
        evt.name = evt.name.trimStart().trimEnd();
        setEvent({ ...evt, [name]: value })
    }

    const formattedDateTime = evt.dateAndTime ? new Date(evt.dateAndTime).toISOString().slice(0, 16) : '';
    return (
        <Segment clearin inverted>
            <Form onSubmit={handleSubmit} autoComplete='off' className='ui form' style={{ display: 'flow-root' }}>
                <Form.Input placeholder='Name' name='name' value={evt.name} onChange={handleImputChange} />
                <Form.TextArea placeholder='Description' name='description' value={evt.description} onChange={handleImputChange} />
                <Form.Input type="datetime-local" placeholder='DateAndTime' name='dateAndTime' value={formattedDateTime} onChange={handleImputChange} />
                <Form.Input placeholder='Venue' name='venue' value={evt.venue} onChange={handleImputChange} />
                <Form.Input placeholder='Category' name='category' value={evt.category} onChange={handleImputChange} />
                <Form.Input type="number" min="1" placeholder='MaxNumberOfParticipants' name='maxNumberOfParticipants' value={evt.maxNumberOfParticipants} onChange={handleImputChange} />
                <Form.Input placeholder='Image' name='image' value={evt.image} onChange={handleImputChange} />
                <Button floated='right' positive type='submit' content='Submit' />
                <Button as={NavLink} to='/' floated='right' type='button' content='Cancel' />
            </Form>
        </Segment>
    )
}