import { createApi } from "@reduxjs/toolkit/query/react";
import { Meal, MealTypeEnum } from "../../../model/Meal";
import { axiosBaseQuery } from "../../services/axiosBaseQuery";

export interface CreateMealCommand {
  name: string;
  type: MealTypeEnum;
}

export const mealApi = createApi({
  reducerPath: "meal",
  tagTypes: ["Meal"],
  baseQuery: axiosBaseQuery({ baseUrl: "https://localhost:5001/api/" }),
  endpoints: (builder) => ({
    getMeals: builder.query<Meal[], void>({
      query: () => ({ url: "meal", method: "GET" }),
      transformResponse: (response: { data: Meal[] }) => response.data,
    }),
    addMeal: builder.mutation<string, CreateMealCommand>({
      query: (body) => ({
        url: `meal`,
        method: "POST",
        body,
      }),
      invalidatesTags: ["Meal"],
      transformResponse: (response: { data: string }) => response.data,
    }),
  }),
});

export const { useAddMealMutation, useGetMealsQuery } = mealApi;
