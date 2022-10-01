import { Action, configureStore, ThunkAction } from "@reduxjs/toolkit";
import layoutReducer from "./slices/layout/layoutSlice";

export const store = configureStore({
  reducer: {
    layout: layoutReducer,
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
