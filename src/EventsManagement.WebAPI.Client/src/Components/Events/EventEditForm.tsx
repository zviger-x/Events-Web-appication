import { useEffect, useState } from "react";
import { useParams, NavLink, useNavigate } from "react-router-dom";
import { EventDTO } from "../../Models/Events/EventDTO";
import APIConnector from "../../API/APIConnector";
import { Button, Form, Segment } from "semantic-ui-react";
import noImg from "../../assets/no_img.jpg";
import "./EventEdit.css";

export default function EventEditForm() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [event, setEvent] = useState<EventDTO>({
        id: undefined,
        name: '',
        venue: '',
        category: '',
        dateAndTime: '',
        currentNumberOfParticipants: 0,
        maxNumberOfParticipants: 1,
        description: '',
        image: ''
    });
    const [loading, setLoading] = useState(true);
    const [errors, setErrors] = useState<{ [key: string]: string }>({});

    useEffect(() => {
        const fetchData = async () => {
            if (id) {
                const fetchedEvent = await APIConnector.GetEventById(parseInt(id));
                setEvent(fetchedEvent!);
                setLoading(false);
            }
        };

        fetchData();
    }, [id]);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        if (event) {
            const { name, value } = e.target;
            setEvent({ ...event, [name]: value });
            setErrors({ }); // Remove errors on change
        }
    };

    const handleImageChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files.length > 0) {
            const file = e.target.files[0];
            const reader = new FileReader();
            reader.onloadend = () => {
                if (event) {
                    // const base64Data = reader.result as string;
                    const base64Data = (reader.result as string).split(",")[1];
                    setEvent({ ...event, image: base64Data });
                }
            };
            reader.readAsDataURL(file);
        }
    };

    const validate = async (): Promise<boolean> => {
        const newErrors: { [key: string]: string } = {};
        if (!event.name) newErrors.name = "Name is required.";
        if (!event.venue) newErrors.venue = "Venue is required.";
        if (!event.category) newErrors.category = "Category is required.";
        if (!event.dateAndTime) newErrors.dateAndTime = "Date and Time is required.";
        if (!event.description) newErrors.description = "Description is required.";
        if (event.maxNumberOfParticipants <= 0) newErrors.maxNumberOfParticipants = "Max number of participants must be greater than 0.";

        // Check for duplicate event name
        if (event?.name) {
            const existingEvents = await APIConnector.GetEvents("name", event.name);
            const isDuplicate = existingEvents.some((existingEvent) => existingEvent.id !== event.id);
            if (isDuplicate) {
                newErrors.name = "An event with this name already exists.";
                newErrors.nameDuplicate = "An event with this name already exists.";
            }
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (await validate()) {
            if (event) {
                console.log(event);
                await APIConnector.EditEvent(event);
                navigate(`/event/${id}`);
            }
        }
    };

    if (loading) {
        return <h1 className="load-text">Loading...</h1>;
    }

    const imageSrc = event?.image ? `data:image;base64,${event.image}` : noImg;

    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Segment className="form-container" style={{ display: 'inline-block', width: 'auto' }}>
                <div style={{ padding: '6px', textAlign: 'start' }}>
                    <h1>Edit event</h1>
                </div>
                <div className="event-image">
                    {imageSrc && <img src={imageSrc} alt="Event"/>}
                </div>
                <div className="event-details">
                    <Form onSubmit={handleSubmit}>
                        <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px' }}>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Image</label>
                                <input
                                    type="file"
                                    accept="image/*"
                                    onChange={handleImageChange}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Name</label>
                                <Form.Input
                                    type="text"
                                    name="name"
                                    value={event.name}
                                    onChange={handleInputChange}
                                    error={!!errors.name}
                                    placeholder={errors.name || ''}
                                />
                                {errors.nameDuplicate && <div style={{ color: 'darkred' }}>{errors.nameDuplicate}</div>}
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Category</label>
                                <Form.Input
                                    type="text"
                                    name="category"
                                    value={event.category}
                                    onChange={handleInputChange}
                                    error={!!errors.category}
                                    placeholder={errors.category || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Venue</label>
                                <Form.Input
                                    type="text"
                                    name="venue"
                                    value={event.venue}
                                    onChange={handleInputChange}
                                    error={!!errors.venue}
                                    placeholder={errors.venue || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Date and Time</label>
                                <Form.Input
                                    type="datetime-local"
                                    name="dateAndTime"
                                    value={event.dateAndTime ? new Date(event.dateAndTime).toISOString().slice(0, 16) : ''}
                                    onChange={handleInputChange}
                                    error={!!errors.dateAndTime}
                                    placeholder={errors.dateAndTime || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Max number of participants</label>
                                <Form.Input
                                    type="number"
                                    min='1'
                                    name="maxNumberOfParticipants"
                                    value={event.maxNumberOfParticipants}
                                    onChange={handleInputChange}
                                    error={!!errors.maxNumberOfParticipants}
                                    placeholder={errors.maxNumberOfParticipants || ''}
                                />
                            </Form.Field>
                            <Form.Field style={{ flex: '1 1 45%' }}>
                                <label>Description</label>
                                <Form.TextArea
                                    name="description"
                                    value={event.description}
                                    onChange={handleInputChange}
                                    rows={3}
                                />
                                {errors.description && <div style={{ color: 'darkred' }}>{errors.description}</div>}
                            </Form.Field>
                        </div>
                        <div style={{ paddingTop: '20px'}}>
                            <Button floated='right' type="submit" color="green">Save</Button>
                            <Button floated='right' as={NavLink} to={`/event/${id}`} color="grey">Cancel</Button>
                        </div>
                    </Form>
                </div>
            </Segment>
        </div>
    );
}