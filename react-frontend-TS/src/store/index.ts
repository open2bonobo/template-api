import thunk from 'redux-thunk';
import { reducer } from "./reducer";
import { configureStore } from "@reduxjs/toolkit";
import { useDispatch,  } from "react-redux";
import { RootStore } from "../types";


export const store = configureStore<RootStore, any>({ reducer, middleware: [thunk], } )

export const useAppDispatch = () => useDispatch<typeof store.dispatch>()
