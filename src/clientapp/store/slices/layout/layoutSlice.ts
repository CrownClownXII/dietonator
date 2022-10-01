import { createSlice, PayloadAction } from "@reduxjs/toolkit";

// declaring the types for our state
type LayoutState = {
  sidebarOpen: boolean;
};

const initialState: LayoutState = {
  sidebarOpen: false,
};

const layoutSlice = createSlice({
  name: "layout",
  initialState,
  reducers: {
    toggleSidebar: (state) => {
      state.sidebarOpen = !state.sidebarOpen;
    },
    closeSidebar: (state) => {
        state.sidebarOpen = false;
    }
  },
});

export const { toggleSidebar, closeSidebar } = layoutSlice.actions;

export default layoutSlice.reducer;
