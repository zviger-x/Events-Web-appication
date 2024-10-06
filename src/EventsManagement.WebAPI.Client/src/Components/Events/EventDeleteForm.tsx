import { useNavigate, useParams } from "react-router-dom";
import APIConnector from "../../API/APIConnector";
import { Button, Segment } from "semantic-ui-react";

export default function EventDeleteForm() {
    const { id } = useParams();
    const navigate = useNavigate();

    const handleDelete = async () => {
        await APIConnector.DeleteEvent(parseInt(id!));
        navigate('/');
    };

    const handleCancel = () => {
        navigate(-1);
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Segment className="form-container" style={{ display: 'inline-block', width: 'auto' }}>
                <h1>Confirm deletion</h1>
                <h1 style={{ fontSize: '18px', marginTop: '10px' }}>Are you sure you want to delete the event?<br/>This action cannot be undone.</h1>
                <div style={{ paddingTop: '10px'}}>
                    <Button floated='right' color="red" onClick={handleDelete}>Delete</Button>
                    <Button floated='right' onClick={handleCancel}>Cancel</Button>
                </div>
            </Segment>
        </div>
    );
}