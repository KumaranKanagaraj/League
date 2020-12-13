import React, { Component } from 'react';
import { Layout } from './components/Layout';
import Home from './components/Home';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<Layout>
				<Home />
			</Layout>
		);
	}
}
