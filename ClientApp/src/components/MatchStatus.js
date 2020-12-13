import React, { useState, useEffect } from 'react';
import axios from 'axios'
import { Alert, Form, Col, Button, InputGroup, Spinner } from 'react-bootstrap';
import autocomplete from 'autocompleter';

import { routeUrl } from '../config/navigator';

const MatchStatus = ({ getLeagueSummary }) => {

	const [winningTeam, setWinningTeam] = useState();
	const [loosingTeam, setLoosingTeam] = useState();
	const [tie, setTie] = useState(false);
	const [showAlert, setShowAlert] = useState(false);
	const [alertMessage, setAlertMesssage] = useState(false);

	const setSuggestion = ({ inputElement, status }) => {
		autocomplete({
			input: inputElement,
			preventSubmit: true,
			debounceWaitMs: 100,
			//emptyMsg: 'No College found',
			className: 'team',
			customize: function (input, inputRect, container, maxHeight) {
				if (maxHeight < 100) {
					container.style.top = "";
					container.style.bottom = (window.innerHeight - inputRect.bottom + input.offsetHeight) + "px";
					container.style.maxHeight = "200px";
				}
			},
			fetch: function (text, update) {
				text = text.toLowerCase();
				axios.get(`${routeUrl.url}/${text}/teams`)
					.then((response) => {
						const data = response.data;
						var suggestion = [];
						data.forEach(item => suggestion.push({ label: item.name, value: item.id }))
						update(suggestion);
					})
			},
			onSelect: function (item) {
				inputElement.value = item.label;
				if (status == 'winningTeam') {
					setWinningTeam(item);
				}
				if (status == 'loosingTeam') {
					setLoosingTeam(item);
				}
			}
		});
	}

	const runAutoComplete = () => {
		setSuggestion({ inputElement: document.getElementById("winning-team"), status: 'winningTeam' })
		setSuggestion({ inputElement: document.getElementById("loosing-team"), status: 'loosingTeam' })

	}
	useEffect(() => {
		runAutoComplete();
	});

	const onTieClick = (event) => {
		setTie(event.target.checked);
	}

	const onSubmit = (event) => {
		console.log(winningTeam);
		console.log(loosingTeam);
		if (!winningTeam || !loosingTeam) {
			setAlertMesssage('Proper team has to be selected')
			setShowAlert(true);
		}
		else if (winningTeam.value == loosingTeam.value) {
			setAlertMesssage('Two team has to be differ')
			setShowAlert(true);
		}
		else {
			setAlertMesssage('')
			setShowAlert(false);
		}
		var matchResult = {
			teamOneId: winningTeam.value,
			teamTwoId: loosingTeam.value,
			winningTeamId: winningTeam.value,
			isTie: tie
		}

		axios.post(`${routeUrl.url}/add/fixture`, matchResult)
			.then((response) => {
				console.log(response);
				getLeagueSummary({ pageNumber: 1 });
				setWinningTeam(); setLoosingTeam(); setTie(false);
			}, (error) => {
				console.log(error);
			});

	}

	return (
		<>
			<Form className="match-status">
				<Form.Row className="align-items-center">
					<Col xs="auto">
						<Form.Label htmlFor="winning-team" srOnly>
							Winning Team
      </Form.Label>
						<Form.Control
							className="mb-2"
							id="winning-team"
							placeholder="Winning Team"
							autoComplete="off"
						/>
					</Col>
					<Col xs="auto">
						<Form.Label htmlFor="loosing-team" srOnly>
							Loosing Team
      </Form.Label>
						<Form.Control
							className="mb-2"
							id="loosing-team"
							placeholder="Loosing Team"
							autoComplete="off"
						/>
					</Col>
					<Col xs="auto">
						<Form.Check
							type="checkbox"
							id="match-tie"
							className="mb-2"
							label="Match Tie"
							onClick={onTieClick}
						/>
					</Col>
					<Col xs="auto">
						<Button className="mb-2" onClick={onSubmit}>Submit Result</Button>
					</Col>
				</Form.Row>
			</Form>
			{showAlert && <Alert variant="danger" onClose={() => setShowAlert(false)} dismissible>{alertMessage}</Alert>}
		</>
	);
};

export default MatchStatus;