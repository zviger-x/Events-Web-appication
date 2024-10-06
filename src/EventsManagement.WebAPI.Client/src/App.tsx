import { Outlet, useLocation } from 'react-router-dom';
import './App.css';
import EventsTable from './Components/Events/EventsTable';
import { Container } from 'semantic-ui-react';
import { useEffect } from 'react';
import { SetupErrorHandlingInterceptor } from './Interceptors/AxiosInterceptor';

function App() {
    const location = useLocation();

    useEffect(() => {
        SetupErrorHandlingInterceptor();
    }, []);

    return (
        <>
            {location.pathname === '/' ? <EventsTable /> : (
                <Container className="container-style">
                    <Outlet />
                </Container>
            )}
        </>
    )
}

export default App;