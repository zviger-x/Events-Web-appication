import { EventDTO } from "../Models/Events/EventDTO";
import { API_BASE_URL } from "../../config"
import axios, { AxiosResponse } from "axios";
import { UserDTO } from "../Models/Users/UserDTO";

const axiosInstance = axios.create({
    baseURL: API_BASE_URL,
});

axiosInstance.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

const APIConnector = {
    GetEvents: async (sortBy?: string, value?: string, page?: string): Promise<EventDTO[]> => {
        const params = new URLSearchParams();
        if (sortBy) params.append('sortby', sortBy);
        if (value) params.append('value', value);
        if (page) params.append('page', page);

        const response: AxiosResponse<EventDTO[]> = await axiosInstance.get(`${API_BASE_URL}/api/events?${params.toString()}`);
        const events = response.data;
        return events;
    },

    CreateEvent: async (event: EventDTO): Promise<void> => {
        await axiosInstance.post<number>(`${API_BASE_URL}/api/Events`, event);
    },

    EditEvent: async (event: EventDTO): Promise<void> => {
        await axiosInstance.put<number>(`${API_BASE_URL}/api/Events/${event.id}`, event);
    },

    DeleteEvent: async (eventId: number): Promise<void> => {
        await axiosInstance.delete<number>(`${API_BASE_URL}/api/Events/${eventId}`);
    },

    GetEventById: async (eventId: number): Promise<EventDTO | undefined> => {
        const response = await axiosInstance.get<EventDTO>(`${API_BASE_URL}/api/Events/${eventId}`);
        return response.data;
    },

    Login: async (email: string, password: string) => {
        const response = await axiosInstance.post(`${API_BASE_URL}/api/account/login`, {
            email,
            password,
        });
        const { accessToken } = response.data;
        localStorage.setItem('token', accessToken);
        // location.reload();
    },

    Logout: () => {
        localStorage.removeItem('token');
    },

    RegisterUser: async (user: UserDTO): Promise<void> => {
        await axiosInstance.post<number>(`${API_BASE_URL}/api/account/register`, user);
    },

    EditUser: async (user: UserDTO): Promise<void> => {
        await axiosInstance.put<number>(`${API_BASE_URL}/api/account/${user.id}`, user);
    },
    
    GetUserById: async (userId: number): Promise<UserDTO | undefined> => {
        const response = await axiosInstance.get<UserDTO>(`${API_BASE_URL}/api/account/${userId}`);
        return response.data;
    },
    
    GetUserByEmail: async (userEmail: string): Promise<UserDTO | undefined> => {
        const response = await axiosInstance.get<UserDTO>(`${API_BASE_URL}/api/account/byemail/${userEmail}`);
        return response.data;
    },

    GetUserEvents: async (userId: number): Promise<EventDTO[]> => {
        const response = await axiosInstance.get<EventDTO[]>(`${API_BASE_URL}/api/account/events/${userId}`);
        const events = response.data;
        return events;
    },
}

export default APIConnector;