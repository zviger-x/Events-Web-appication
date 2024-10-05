import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import 'semantic-ui-css/semantic.min.css';
import App from './App';
import './App.css'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <div className="app-tab-container">
            <h1>
                Events management application
            </h1>
        </div>
        <App />
        <div style={{margin: '150px', minHeight: '150px' }} />
    </StrictMode>,
)