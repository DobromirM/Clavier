import React from 'react';
import { SwitchButton } from './SwitchButton';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<SwitchButton />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});

it('check if the role switch function is defined', () => {
    expect(SwitchButton.prototype.requestRoleSwitch).not.toBeNull();
});