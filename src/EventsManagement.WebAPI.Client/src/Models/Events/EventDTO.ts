export interface EventDTO {
    id: number,
    name: string,
    description: string,
    dateAndTime: string,
    venue: string,
    category: string,
    currentParticipants: number,
    maxNumberOfParticipants: number,
    image: string | null,
}