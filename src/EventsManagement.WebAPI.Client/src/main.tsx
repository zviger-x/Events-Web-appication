import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import 'semantic-ui-css/semantic.min.css';
import './App.css'
import { RouterProvider } from 'react-router';
import { Router } from './Router/Routes';

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <div className="app-tab-container">
            <h1> Events management application </h1>
        </div>
        <RouterProvider router={ Router } />
        <div style={{margin: '150px', minHeight: '150px' }} />
    </StrictMode>,
)