export interface EventDTO {
    id: number | undefined,
    name: string,
    description: string,
    dateAndTime: string | undefined,
    venue: string,
    category: string,
    currentNumberOfParticipants: number,
    maxNumberOfParticipants: number,
    image: string | null,
}