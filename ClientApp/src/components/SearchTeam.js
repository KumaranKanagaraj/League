import React, { useState, useEffect } from 'react';
import axios from 'axios'
import { FormControl, Form, Col, Button, Spinner } from 'react-bootstrap';

const SearchTeam = ({ getLeagueSummary }) => {

	const [teamName, setTeamName] = useState('');

	const onTeamNameChange = (event) => {
			setTeamName(event.target.value);
	}

	const onSubmit = () => {
		getLeagueSummary({ pageNumber: 1, teamName: teamName });
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
						<Button className="mb-2"  onClick={onSubmit}>Search</Button>
					</Col>
				</Form.Row>
			</Form>
		</>
	);
};

export default SearchTeam;