export interface ChatMessage {
    id?: string;
    createdAt?: Date;
    question?: string;
    answer?: string;
    isLiked?: boolean;
    isGenerated?: boolean;
}