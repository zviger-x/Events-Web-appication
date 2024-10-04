import { useEffect, useState } from 'react';
import './App.css';

interface Event {
    id: number,
    name: string,
    description: string,
    dateAndTime: string,
    venue: string,
    category: string,
    maxNumberOfParticipants: number,
    image: string
}

function App() {
    const [events, setEvents] = useState<Event[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        console.log('Fetching events from API...');
        // Запрашиваем события с API
        fetch('/api/events/getall')
            .then(response => {
                console.log('Response:', response);
                if (!response.ok) {
                    throw new Error('Error fetching events');
                }
                return response.json();
            })
            .then(data => {
                console.log('Data:', data);
                setEvents(data);
            })
            .catch(error => {
                console.error('Error fetching events:', error);
                setError(error.message);
            });
    }, []);

    return (
        <>
            {error ? (
                <p>Error: {error}</p>
            ) : (
                <div>
                        {events.map((event) => (
                            <tr key={event.id}>
                                <td>{event.id}</td>
                                <td>{event.name}</td>
                                <td>{event.description}</td>
                                <td>{new Date(event.dateAndTime).toLocaleDateString()}</td>
                                <td>{event.venue}</td>
                                <td>{event.category}</td>
                                <td>{event.maxNumberOfParticipants}</td>
                                <td>{event.image}</td>
                            </tr>
                        ))}
                </div>
            )}
        </>
    );
}

export default App;