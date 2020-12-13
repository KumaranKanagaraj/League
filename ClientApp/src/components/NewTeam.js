import React, { useState, useEffect } from 'react';
import axios from 'axios'
import { Toast, Form, Col, Button, Spinner } from 'react-bootstrap';

import { routeUrl } from '../config/navigator';


const NewTeam = () => {

    const [teamName, setTeamName] = useState('');
    const [toastShow, setToastShow] = useState(false);

    const onTeamNameChange = (event) => {
        if (event.target.value && event.target.value && event.target.value.length != 0 && !(/^\s*$/.test(event.target.value))) {
            setTeamName(event.target.value);
        }
    }

    const onSubmit = () => {
        axios.post(`${routeUrl.url}/add/team/${teamName}`)
            .then((response) => {
                setToastShow(true);
                console.log(response);
                setTeamName('');
            }, (error) => {
                console.log(error);
            });

	}

	return (
		<>
            <Form className="add-team">
                <Form.Row className="align-items-center">
                    <Col xs="auto">
                        <Form.Label htmlFor="add-team" srOnly>
                            Team Name
      </Form.Label>
                        <Form.Control
                            className="mb-2"
                            id="add-team"
                            placeholder="Team Name"
                            onChange={onTeamNameChange}
                        />
                    </Col>
                    <Col xs="auto">
                        <Button className="mb-2" disabled={!teamName} onClick={onSubmit}>Add Team</Button>
                    </Col>
                </Form.Row>
            </Form>
            <Toast onClose={() => setToastShow(false)} show={toastShow} delay={3000} autohide>
                <Toast.Body>Successfully added :)</Toast.Body>
            </Toast>
		</>
	);
};

export default NewTeam;