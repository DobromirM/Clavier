import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import ReactTestUtils from 'react-dom/test-utils';
import ShallowRenderer from 'react-test-renderer/shallow';

it('renders without crashing', () => {
  const div = document.createElement('div');
    ReactDOM.render(<App />, div);
});

it('check if it is composite component', () => {
    ReactTestUtils.isCompositeComponent(<App />);
})

it('check if it renders children', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<App />);
    const result = renderer.getRenderOutput();
    expect(result.props.children).not.toBeNull();
});