import { EventDTO } from "../Models/Events/EventDTO";
import { GetEventsResponse } from "../Models/Events/GetEventsResponse";
import { API_BASE_URL } from "../../config"
import { GetEventByIdResponse } from "../Models/Events/GetEventByIdResponse";
import axios, { AxiosResponse } from "axios";

const APIConnector = {
    GetEvents: async (): Promise<EventDTO[]> => {
        try {
            const response: AxiosResponse<GetEventsResponse> = await axios.get(`${API_BASE_URL}/api/events`);
            const events = response.data.map(e => ({
                ...e,
                currentParticipants: 0, // Здесь я должен получить количество пользователей через апи ивентпользователей
            }));
            return events;
        }
        catch (error) {
            console.log(`Error fetching events: `, error);
            throw error;
        }
    },

    CreateEvent: async (event: EventDTO): Promise<void> => {
        try {
            await axios.post<number>(`${API_BASE_URL}/api/Events`, event);
        }
        catch (error) {
            console.log(`Error fetching events: `, error);
            throw error;
        }
    },

    EditEvent: async (event: EventDTO): Promise<void> => {
        try {
            await axios.put<number>(`${API_BASE_URL}/api/Events`, event);
        }
        catch (error) {
            console.log(`Error fetching events: `, error);
            throw error;
        }
    },

    DeleteEvent: async (eventId: number): Promise<void> => {
        try {
            await axios.delete<number>(`${API_BASE_URL}/api/Events/${eventId}`);
        }
        catch (error) {
            console.log(`Error fetching events: `, error);
            throw error;
        }
    },

    GetEventById: async (eventId: number): Promise<EventDTO | undefined> => {
        try {
            const response = await axios.get<GetEventByIdResponse>(`${API_BASE_URL}/api/Events/${eventId}`);
            return response.data.Event;
        }
        catch (error) {
            console.log(`Error fetching events: `, error);
            throw error;
        }
    }
}

export default APIConnector;