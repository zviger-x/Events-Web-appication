import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import 'semantic-ui-css/semantic.min.css';
import './App.css'
import { RouterProvider } from 'react-router';
import { Router } from './Router/Routes';

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={ Router } />
    </StrictMode>,
)