export enum MealTypeEnum {
    calculableMeal = 0,
    template = 1
}

export interface Meal {
    id: string;
    name: string;
    type: MealTypeEnum;
    kcal: number;
    proteins: number;
    fats: number;
    carbohydrate: number;
}